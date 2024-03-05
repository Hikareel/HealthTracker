import { reactive } from "vue";


//Użycie DOMPurify do serializacji HTML'a wpisywanego przez użytkowników.
const PostData = reactive([
    {
        user: "user1",
        markdownText: "# Hello world\n\nSome plain text"

    },
    {
        user: "user2",
        markdownText: "# *Hello* world\nSome **plain** text"

    },
    {
        user: "user3",
        markdownText: "# Hello world\n\nSome plain text"

    }
])

interface IPostModel {
    user: string,
    markdownText: string
}
export { PostData };
export type { IPostModel };