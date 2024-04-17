<template>
    <div class="border">
        <div class="main-box">
            <div class="title">{{ item.name }}</div>
            <div class="content">
                <div class="description">
                    {{ item.description }}
                </div>
                <div class="main-box-content">
                    <div class="content-label">{{ item.label }}</div>
                    <div class="content-value">
                        <div v-for="(val, index) in item.fieldValue" :key="index">
                            {{ val }}
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</template>

<script setup lang="ts">
import { defineProps } from "vue";
import type { IHomeCardModel } from "@/data/models/homeCardModel";

defineProps<{
    item: IHomeCardModel;
}>();
</script>

<style scoped lang="scss">
.border {
    font-family: "Trebuchet MS", "Lucida Sans Unicode", "Lucida Grande",
        "Lucida Sans", Arial, sans-serif;
    border: 2px solid rgba(37, 32, 32, 1);
    -webkit-box-shadow: 5px 5px 5px 5px #808080;
    -moz-box-shadow: 5px 5px 5px 5px #808080;
    box-shadow: inset 0px 0px 50px 20px rgba(37, 32, 32, 1);
    background-color: rgba(62, 50, 50, 1);
    height: inherit;

    .main-box {

        .title {
            padding: 1rem;
            font-family: cursive;
            font-size: 30px;
            word-spacing: 4px;
            color: #ffffff;
            text-shadow: 1px 1px 3px rgba(121, 121, 121, 1);
            font-weight: 700;
        }

        .content {
            transition: opacity 0.25s ease, font-size 0.25s ease-out;
            font-size: 0;
            opacity: 0;

            .description {
                text-align: left;
                text-align: justify;
                -webkit-line-clamp: 2;
                -webkit-box-orient: vertical;
                overflow: hidden;
                text-overflow: ellipsis;
                white-space: normal;
                max-height: 3em;
                line-height: 1.5em;
                color: #dddddd;
            }

            .main-box-content {
                display: grid;
                grid-template-rows: 1fr;
                grid-template-columns: repeat(2, 1fr);

                .content-label {
                    justify-content: left;
                }

                .content-value {
                    justify-content: right;
                }
            }
        }

    }
}
</style>
