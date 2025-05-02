
export type ResponseResult<T> = {
    success: boolean;
    data?: T;
    error?: ErrorDetail
}

export interface ErrorDetail{
    error_code?: string;
    error_message?: string;
    log_id?: string;
}