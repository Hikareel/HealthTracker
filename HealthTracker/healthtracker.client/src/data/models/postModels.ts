import { reactive } from "vue";


//Użycie DOMPurify do serializacji HTML'a wpisywanego przez użytkowników.
const PostData = reactive([
    {
        user: "user1",
        markdownText: "<h1>Hello world</h1></br>Some plain text"
    },
    {
        user: "user2",
        markdownText: "<h1>Hello world</h1></br>Some plain text"
    },
    {
        user: "user3",
        markdownText: "<h1>Hello world</h1></br>Some plain text"
    }
])

interface IPostModel {
    user: string,
    markdownText: string
}
export { PostData };
export type { IPostModel };