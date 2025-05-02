interface Window {
  instanceId: string;
  wixEmbedsAPI: {
    getMetaSiteId: () => string
  }
}

declare interface String {
  format(...args: string[]): string;
  thenResultErrorIf(error_message: string): ResponseResult<any>;
}
