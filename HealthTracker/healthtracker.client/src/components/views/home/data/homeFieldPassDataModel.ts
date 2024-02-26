interface HomeFieldPassModel {
    [key: string]: string | string[] | { [key: string]: any };
    Title: string;
    Description: string;
    Label: string;
    FieldValue: string[],
  }
  
  
  export type { HomeFieldPassModel }

//   interface LoginModel {
//     EmailUserName: string,
//     Password: string,
//   }
  
//   export type { LoginModel }