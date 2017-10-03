
export class ResponseBase<T> {

    constructor(
        public data: T,
        public status: number,
        public message: string) {
    }
}                     