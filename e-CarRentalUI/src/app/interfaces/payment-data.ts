export interface PaymentData {
    id: number;
    rentalId: number;
    description: string;
    sum: number;
    paymentDate: string;
    userId: string;
    userFullName?: string;
}
