<template>
    <div class="form">
        <FormStatus formTitle="New Password"/>
        <Vueform class="form-content" v-model="formData" @submit="sendFormData" :float-placeholders="false"
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
import type { INewPassModel } from '@/data/models/formDataModels';
import FormStatus from '@/components/shared/FormStatus.vue'
import { preventSubmit } from '@/service/api/account/sendDataService'

const formData = ref<INewPassModel>({
    password: '',
    password_confirmation: '',
});

const sendFormData = async () => {
    preventSubmit("/new-pass", JSON.stringify(formData.value))
    formData.value.password = ''
    formData.value.password_confirmation = ''
}
</script>

<style lang="scss" scoped>
</style>