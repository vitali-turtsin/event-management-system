import http from "../http-common";
import { IPaymentMethodSearch } from "../models/searches/IPaymentMethodSearch";
import { IPaymentMethod } from "../models/IPaymentMethod";
import { handleHttpError } from "../shared/helpers/http.helper";
import { toQueryString } from "../shared/helpers/http-request-util";

const PAYMENT_METHODS_URL = "/PaymentMethods";

const getAll = async (search?: IPaymentMethodSearch) => {
  try {
    return await http.get<IPaymentMethod[]>(
      `${PAYMENT_METHODS_URL}?${toQueryString(search)}`
    );
  } catch (error) {
    return handleHttpError(error);
  }
};

const get = async (id: string) => {
  try {
    return await http.get<IPaymentMethod>(`${PAYMENT_METHODS_URL}/${id}`);
  } catch (error) {
    return handleHttpError(error);
  }
};

const post = async (entity: IPaymentMethod) => {
  try {
    return await http.post<IPaymentMethod>(PAYMENT_METHODS_URL, entity);
  } catch (error) {
    return handleHttpError(error);
  }
};

const put = async (entity: IPaymentMethod, id: string) => {
  try {
    return await http.put<IPaymentMethod>(
      `${PAYMENT_METHODS_URL}/${id}`,
      entity
    );
  } catch (error) {
    return handleHttpError(error);
  }
};

const remove = async (id: string) => {
  try {
    return await http.delete<IPaymentMethod>(`${PAYMENT_METHODS_URL}/${id}`);
  } catch (error) {
    return handleHttpError(error);
  }
};

const PaymentMethodService = {
  getAll,
  get,
  post,
  put,
  remove,
};

export default PaymentMethodService;
