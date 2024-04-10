export const toQueryString = <TSearch>(search?: TSearch): string => {
    const params = new URLSearchParams();
    if (!search) return '';

    for (const key in search) {
        if (search[key] !== undefined && search[key] !== null) {
            params.append(key, String(search[key]));
        }
    }
    return params.toString();
}
