
export class GenericSimpleResult {
    errors: string[];
    success: boolean;
}

export class  GenericResult<TResult> extends GenericSimpleResult{
    public result : TResult;
}
