<template>
    <div class="form">
        <FormStatus formTitle="Reset Password"/>
        <Vueform class="form-content" v-model="formData" @submit="sendFormData" :float-placeholders="false"
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
import type { IPassResetModel } from "@/data/models/formDataModels";
import FormStatus from '@/components/shared/FormStatus.vue'
import { preventSubmit } from '@/service/api/account/sendDataService'

const formData = ref<IPassResetModel>({
    email: "",
});

const sendFormData = async () => {
    preventSubmit("/pass-reset", JSON.stringify(formData.value))
    formData.value.email = "";
};
</script>

<style lang="scss" scoped>
</style>
