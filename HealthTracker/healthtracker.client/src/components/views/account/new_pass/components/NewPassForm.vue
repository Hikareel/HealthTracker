<template>
    <div class="form">
        <p class="form-label">New Password</p>
        <p v-for="msg in er" class="error" v-bind:key="msg">
            {{ msg }}
        </p>
        <p class="success">
            {{ isChanged }}
        </p>
        <Vueform class="form-content" v-model="formData" @submit="preventSubmit" :float-placeholders="false"
            :endpoint="false" :display-errors="false" sync>
            <GroupElement name="password">
                <TextElement info="Password and Confirm password must match" name="Password" label="Password"
                    placeholder="Password" input-type="password"
                    rules="required|confirmed|min:6|regex:/^(?=.*[^\w\d])(?=.*\d)(?=.*[A-Z]).+$/" :messages="{
                        regex: 'At least one character of type: alphanumeric, capital letter, number'
                    }" :addons="{
    before: `<i class='bi bi-lock-fill'></i>`
}" />
                <TextElement name="Password_confirmation" placeholder="Confirm password" input-type="password"
                    rules="required" :addons="{
                        before: `<i class='bi bi-lock-fill'></i>`
                    }" />
            </GroupElement>
            <GroupElement name="controll">
                <ButtonElement id="reset_button" name="reset" type="reset" :resets="true" hidden />
                <ButtonElement name="submit" button-label="Confirm" align="right" :submits="true" full size="lg" :columns="{
                    container: 12,
                    label: 12
                }" />
            </GroupElement>
        </Vueform>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type { NewPassModel } from '../data/newPassModel';
import axios from 'axios';


const er = ref<string[]>([])
const isChanged = ref("")

const formData = ref<NewPassModel>({
    password: '',
    password_confirmation: '',
});

const preventSubmit = async () => {
    er.value.splice(0, er.value.length)
    isChanged.value = ''
    let response
    try {
        const { data } = await axios.post(
            '/api/register',
            JSON.stringify(formData.value),
            {
                headers: {
                    "Content-Type": "application/json",
                },
            }
        );
        response = data
        isChanged.value = response.message
        document.getElementById('reset_button')!.click()
    } catch (error: any) {
        formData.value.password = ''
        formData.value.password_confirmation = ''
        error.response.data.forEach((element: { description: string; }) => {
            er.value.push(element.description)
        });
    }
}
</script>

<style lang="scss" scoped>
@use '@/assets/form';
</style>