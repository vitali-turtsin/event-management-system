import http from "../http-common";
import { IPersonSearch } from "../models/searches/IPersonSearch";
import { IPerson } from "../models/IPerson";
import { handleHttpError } from "../shared/helpers/http.helper";
import { toQueryString } from "../shared/helpers/http-request-util";

const PERSONS_URL = "/Persons";

const getAll = async (search?: IPersonSearch) => {
  try {
    return await http.get<IPerson[]>(`${PERSONS_URL}?${toQueryString(search)}`);
  } catch (error) {
    return handleHttpError(error);
  }
};

const get = async (id: string) => {
  try {
    return await http.get<IPerson>(`${PERSONS_URL}/${id}`);
  } catch (error) {
    return handleHttpError(error);
  }
};

const post = async (entity: IPerson) => {
  try {
    return await http.post<IPerson>(PERSONS_URL, entity);
  } catch (error) {
    return handleHttpError(error);
  }
};

const put = async (entity: IPerson, id: string) => {
  try {
    return await http.put<IPerson>(`${PERSONS_URL}/${id}`, entity);
  } catch (error) {
    return handleHttpError(error);
  }
};

const remove = async (id: string) => {
  try {
    return await http.delete<IPerson>(`${PERSONS_URL}/${id}`);
  } catch (error) {
    return handleHttpError(error);
  }
};

const PersonService = {
  getAll,
  get,
  post,
  put,
  remove,
};

export default PersonService;
