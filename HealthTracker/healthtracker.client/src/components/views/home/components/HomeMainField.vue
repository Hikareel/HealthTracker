<template>
    <main>
        <!-- <RouterLink v-for="(link, index) in Object.values(Links).slice(1)"
                    :key="index" :to="`${link.link}`" class="segment"> -->
        <div class="border">
            <div class="main-box">
                <div class="top-left">top-left</div>
                <div class="top-right">top-right</div>
                <div class="bottom-left">bottom-left</div>
                <div class="bottom-right">bottom-right</div>
            </div>
        </div>
    </main>
</template>

<script lang="ts" setup>

</script>

<style scoped lang="scss">
.border {
    padding: 20px;
    border-radius: 25px 5px;
    width: 35rem;
    height: 20rem;
    border: 4px solid #000000;
     -webkit-box-shadow:5px 5px 5px 5px #808080;
     -moz-box-shadow:5px 5px 5px 5px #808080;
     box-shadow:5px 5px 5px 5px #808080;
}

.main-box {
    position: relative;
    display: flex;
    flex-wrap: wrap;
}

.main-box>div {
    width: 45%;
    height: 45%;
}

.top-left {
    align-self: flex-start;
}

.top-right {
    align-self: flex-start;
}

.bottom-left {
    align-self: flex-end;
}

.bottom-right {
    align-self: flex-end;
}
</style>
