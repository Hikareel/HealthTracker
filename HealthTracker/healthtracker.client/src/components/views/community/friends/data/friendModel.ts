import { reactive } from "vue";

//Pobierane z backendu.
const FriendsData = reactive([
    {
        firstname: "ppl1_first",
        secondname: "ppl1_second"
    },
    {
        firstname: "ppl2_first",
        secondname: "ppl2_second"
    },
    {
        firstname: "ppl3_first",
        secondname: "ppl3_second"
    }
])

interface FriendsModel {
    firstname: string;
    secondname: string;
}
export { FriendsData };
export type { FriendsModel };
