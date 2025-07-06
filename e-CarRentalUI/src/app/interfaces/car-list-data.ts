export interface CarListData {
    id: number;
    fullName: string;
    bodyTypeId: number;
    yearOfProduction: number;
    vin: string;
    registration?: string;
    price: number;
    pricePerHour: number;
}
