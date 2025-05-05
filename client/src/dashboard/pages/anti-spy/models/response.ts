import { Guid } from "../domain/domain.model";

export type ResponseResult<T> = {
    success: boolean;
    data?: T;
    error?: ErrorDetail
}

export class ApiResponse {
    static Success = (data?: any): ResponseResult<any> => {
        return {
            success: true,
            data: data
        }
    }
    static Error = (error_code: string, error_message: string) : ResponseResult<any> => {
        return {
            success: false,
            error: {
                error_code: error_code,
                error_message: error_message,
                log_id: Guid.NewGuid()
            }
        }
    }
}

export interface ErrorDetail{
    error_code?: string;
    error_message?: string;
    log_id?: string;
}