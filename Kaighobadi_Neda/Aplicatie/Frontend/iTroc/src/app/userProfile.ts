import {Adress} from "./adress";
export class UserProfile {
    constructor(
        public userId:string,
        public description: string,
        public picURL: string,
    ) { }
    public changeDescription(description:string){
        this.description=description;
    }
    

}