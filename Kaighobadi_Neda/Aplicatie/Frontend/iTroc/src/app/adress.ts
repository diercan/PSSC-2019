 export class Adress{
     
    constructor(
        public city:string,
        public country: string
    )
    {
    }
    public changeCity(city: string){
        this.city=city;
    }
    public changeCountry(country: string){
        this.country=country;
    }
}