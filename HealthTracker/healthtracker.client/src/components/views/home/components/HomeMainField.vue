<template>
    <main>
        <div class="border">
            <div class="main-box">
                <div class="title">{{ title }}</div>
                <div class="description">{{ description }}</div>
                <div class="main-box-content">
                    <div class="content-label">{{ label }}</div>
                    <div class="content-value">
                        <div v-for="(val, index) in fieldValue" :key="index">{{ val }}</div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>

<script lang="ts">
export default {
    props: {
        title: String,
        description: String,
        label: String,
        fieldValue: Array
    }
}

</script>

<style scoped lang="scss">
.border {
    font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    padding: 0px 20px 20px 20px;
    width: fit-content;
    height: fit-content;
    border: 2px solid rgba(37,32,32,1);
    -webkit-box-shadow: 5px 5px 5px 5px #808080;
    -moz-box-shadow: 5px 5px 5px 5px #808080;
    box-shadow: inset 0px 0px 50px 20px rgba(37,32,32,1);
    max-width: 30rem;
    background-color: rgba(62,50,50,1);
}

.main-box {
    display: grid;
    grid-template-rows: repeat(3, 1fr);
    grid-template-columns: 1fr;

}

.main-box-content {
    display: grid;
    grid-template-rows: 1fr;
    grid-template-columns: repeat(2, 1fr);

}

.title {
    padding: 1rem;
    font-family: cursive;
    font-size: 30px;
    word-spacing: 4px;
    color: #FFFFFF;
    text-shadow: 1px 1px 3px rgba(121, 121, 121, 1);
    font-weight: 700;
}

.description {
    text-align: left;
    text-align: justify;
    display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: normal;
    max-height: 3em;
    line-height: 1.5em;
    color:#dddddd
}

.content-label {
    justify-content: left;
}

.content-value {
    justify-content: right;
}

.top-left {
    align-self: flex-start;
}</style>
