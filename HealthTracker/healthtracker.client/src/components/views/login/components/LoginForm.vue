<template>
    <div class="login-main">
        <div class="login-form">
            <p class="login-label">Login</p>
            <p v-for="msg in er" class="error_msg" v-bind:key="msg">
                {{ msg }}
            </p>
            <Vueform id="form" v-model="formData" @submit="preventSubmit" :float-placeholders="false" :endpoint="false"
                :display-errors="false" sync>
                <GroupElement name="email_username">
                    <TextElement name="EmailUsername" label="Email od username" placeholder="user@domain.com/Username"
                        rules="required" :addons="{
                            before: `<i class='bi bi-pencil-fill'></i>`
                        }" />
                </GroupElement>
                <GroupElement name="password">
                    <TextElement name="Password" label="Password" placeholder="Password" input-type="password"
                        rules="required|confirmed|min:6|regex:/^(?=.*[^\w\d])(?=.*\d)(?=.*[A-Z]).+$/" :addons="{
                            before: `<i class='bi bi-lock-fill'></i>`
                        }" />
                </GroupElement>
                <GroupElement name="controll">
                    <ButtonElement name="submit" button-label="Login" align="center" :submits="true" />
                </GroupElement>
            </Vueform>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const er = ref<string[]>([])
</script>
    
<style></style>