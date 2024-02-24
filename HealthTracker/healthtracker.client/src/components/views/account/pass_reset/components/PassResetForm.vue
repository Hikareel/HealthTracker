<template>
    <div class="form">
        <p class="form-label">Reset Password</p>
        <p v-for="msg in er" class="error" v-bind:key="msg">
            {{ msg }}
        </p>
        <p class="success">
            {{ isEmailSent }}
        </p>
        <Vueform class="form-content" v-model="formData" @submit="preventSubmit" :float-placeholders="false"
            :endpoint="false" :display-errors="false" sync>
            <GroupElement name="password">
                <TextElement name="Email" label="Email" placeholder="user@example.com" input-type="email" rules="required|email"
                    :addons="{
                        before: `<i class='bi bi-envelope-at-fill'></i>`
                    }" />
            </GroupElement>
            <GroupElement name="controll">
                <ButtonElement id="reset_button" name="reset" type="reset" :resets="true" hidden />
                <ButtonElement name="submit" button-label="Send reset email" align="right" :submits="true" full size="lg" :columns="{
                    container: 12,
                    label: 12,
                }" />
            </GroupElement>
        </Vueform>
    </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import type { PassResetModel } from "../data/passResetModel";
import axios from "axios";

const er = ref<string[]>([]);
const isEmailSent = ref("");

const formData = ref<PassResetModel>({
    email: "",
});

const preventSubmit = async () => {
    er.value.splice(0, er.value.length);
    isEmailSent.value = "";
    let response;
    try {
        const { data } = await axios.post(
            "/api/register",
            JSON.stringify(formData.value),
            {
                headers: {
                    "Content-Type": "application/json",
                },
            }
        );
        response = data;
        isEmailSent.value = response.message;
        document.getElementById("reset_button")!.click();
    } catch (error: any) {
        formData.value.email = "";
        error.response.data.forEach((element: { description: string }) => {
            er.value.push(element.description);
        });
    }
};
</script>

<style lang="scss" scoped>
@use "@/assets/form";
</style>
