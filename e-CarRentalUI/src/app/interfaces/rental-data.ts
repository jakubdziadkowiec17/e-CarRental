export interface RentalData {
    id: number;
    clientId: number;
    clientFullName?: string | null;
    userId: string;
    userFullName?: string | null;
    carId: number;
    fullNameWithYear?: string | null;
    hours: number;
    createdDate?: string | null;
    startDate: string;
    endDate?: string | null;
    isPaid?: string | null;
    paymentStatus?: string | null;
}
