export class SubscribeRequest {
  constructor(
    public lastname: string | undefined,
    public firstname: string | undefined,
    public email: string | undefined,
    public password: string | undefined
  ) { }
}
