
export class Product {
    constructor(
        public pic:string,
        public name:string,
        public category: string,
        public subcategory: string,
        public description:string,
        public price: number,
        public sellerId:string,
        public sellerAdress: string,
        public id?:string
    ) { }
    
    public editProduct(pic:string,name:string,category:string,subcategory:string,description:string,price:number){
        this.pic=pic;
        this.name=name;
        this.category=category;
        this.subcategory=subcategory;
        this.description=description;
        this.price=price;
    }
    public toProduct(product:any):Product{
        return new Product(product.pic,product.name,product.category,product.subcategory,product.description,product.price,product.sellerId,product.sellerAdress,product.id)
    }
}