import { reactive } from "vue";


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

    },
    {
        user: "user4",
        markdownText: "# Hello world\n\nSome plain text"

    },
    {
        user: "user5",
        markdownText: "# Hello world\n\nSome plain text"

    },
    {
        user: "user6",
        markdownText: "# Hello world\n\nSome plain text"

    }
])

interface IPostModel {
    user: string,
    markdownText: string
}
export { PostData };
export type { IPostModel };