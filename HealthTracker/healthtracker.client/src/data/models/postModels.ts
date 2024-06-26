import { type Ref, ref } from "vue";

const currentPosts: Ref<ICurrentPosts> = ref({
  posts: [],
  pageNumber: 1,
  pageSize: 10,
});

interface IPost {
  id: number;
  userId: number;
  userFirstName: string;
  userLastName: string;
  content: string;
  dateOfCreate: string;
  amountOfComments: number;
  likes: ILike[];
}

interface IComment {
  id: number;
  postId: number;
  userId: number;
  userFirstName: string;
  userLastName: string;
  parentCommentId: number | null;
  amountOfChildComments: number;
  content: string;
  dateOfCreate: string;
}

interface ILike {
  userId: number;
  postId: number;
}

interface ICurrentPosts {
  posts: IPost[];
  pageNumber: number;
  pageSize: number;
}
export { currentPosts };
export type { IPost, ILike, IComment, ICurrentPosts };
