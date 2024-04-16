interface IUrl {
  name: string;
  url: string;
}

interface IAuthor {
  firstName: string;
  lastName: string;
  role: string;
  image: string;
  urls: IUrl[];
}

const authors = [
  {
    firstName: "string",
    lastName: "string",
    role: "string",
    image: "string",
    urls: [
      {
        name: "string",
        url: "string",
      },
      {
        name: "string",
        url: "string",
      },
    ],
  },
  {
    firstName: "string",
    lastName: "string",
    role: "string",
    image: "string",
    urls: [
      {
        name: "string",
        url: "string",
      },
    ],
  },
];

export { authors };
export type { IAuthor, IUrl };
