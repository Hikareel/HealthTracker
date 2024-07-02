<template>
    <div class="loading">
        <div class="loading-container" ref="loadingContainer">
            <p ref="loadingText">Loading</p>
            <img src="@/assets/loading.gif" alt="" @load="adjustFontSize">
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';

const loadingContainer = ref(null);
const loadingText = ref(null);

const adjustFontSize = () => {
    if (loadingContainer.value && loadingText.value) {
        const imgHeight = loadingContainer.value.clientHeight;
        loadingText.value.style.fontSize = `${imgHeight}px`;
    }
};

onMounted(() => {
    adjustFontSize();
});
</script>

<style scoped lang="scss">
.loading {
    opacity: 0.6;
    height: inherit;
    padding: 1%;

    .loading-container {
        justify-content: center;
        gap: 1%;
        align-items: center;
        display: flex;
        height: 100%;
        width: inherit;

        p {
            display: flex;
            align-items: center;
        }

        img {
            height: 100%;
            width: auto;
        }
    }
}
</style>
