export type SignUpUserData = SignInUserData & Required<{
    repeatPassword: String
}>

export type SignInUserData = Required<{
    email: String
    password: String
}>