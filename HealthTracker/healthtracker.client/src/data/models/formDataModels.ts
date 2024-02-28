interface LoginModel {
  EmailUserName: string,
  Password: string,
}

interface NewPassModel{
  password: string,
  password_confirmation: string
}

interface PassResetModel{
  email: string
}

interface RegisterModel {
  Email: string,
  UserName: string,
  FirstName: string,
  LastName: string,
  PhoneNumber: string,
  DateOfBirth: string,
  Password: string,
  Password_confirmation: string
}

export type { LoginModel, NewPassModel, PassResetModel, RegisterModel }