
import {Adress} from "./adress";
export class User {
    constructor(
       // public userId:number,
        public name: string,
        public phone: string,
        public email: string,
        public username: string,
        public password:string,
        public adress: Adress,
        public isSeller: boolean
    ) { }
    public changePassword(password: string)
    {
        this.password=password;
    }
    public changeAdress(city: string, country: string){
        this.adress.changeCity(city);
        this.adress.changeCountry(country);

    }
}