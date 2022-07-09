export class AddOrEditUserModel {
  id?: number;
  email!: string;
  firstName!: string;
  lastName!: string;
  roleid!: number;
  name?: string;
}

export default class UserModel {
  id?: number;
  email!: string;
  firstName!: string;
  lastName!: string;
  roleid!: number;
  role?: string;
  password?: string;
}
