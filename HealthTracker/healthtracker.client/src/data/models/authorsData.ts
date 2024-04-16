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
    firstName: "Damian",
    lastName: "Subzda",
    role: "Developer",
    image: "programista1.png",
    urls: [
      {
        name: "Github",
        url: "https://github.com/DamianSubzda",
      },
      {
        name: "Example",
        url: "https://wp.pl",
      },
    ],
  },
  {
    firstName: "Filip",
    lastName: "Stańczak",
    role: "Developer",
    image: "programista2.png",
    urls: [
      {
        name: "Github",
        url: "https://github.com/Hikareel",
      },
    ],
  },
];

export { authors };
export type { IAuthor, IUrl };
