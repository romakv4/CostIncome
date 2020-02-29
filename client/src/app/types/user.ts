export type ResetPassUserData = Required<{
    email: string;
}>

export type SignInUserData = ResetPassUserData & Required<{
    password: string;
}>

export type SignUpUserData = SignInUserData & Required<{
    repeatPassword: string;
}>