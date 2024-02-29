interface ILoginModel {
  EmailUserName: string,
  Password: string,
}

interface INewPassModel{
  password: string,
  password_confirmation: string
}

interface IPassResetModel{
  email: string
}

interface IRegisterModel {
  Email: string,
  UserName: string,
  FirstName: string,
  LastName: string,
  PhoneNumber: string,
  DateOfBirth: string,
  Password: string,
  Password_confirmation: string
}

export type { ILoginModel, INewPassModel, IPassResetModel, IRegisterModel }