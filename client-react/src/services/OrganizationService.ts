import http from "../http-common";
import { IOrganizationSearch } from "../models/searches/IOrganizationSearch";
import { IOrganization } from "../models/IOrganization";
import { handleHttpError } from "../shared/helpers/http.helper";
import { toQueryString } from "../shared/helpers/http-request-util";

const ORGANIZATIONS_URL = "/Organizations";

const getAll = async (search?: IOrganizationSearch) => {
  try {
    return await http.get<IOrganization[]>(
      `${ORGANIZATIONS_URL}?${toQueryString(search)}`
    );
  } catch (error) {
    return handleHttpError(error);
  }
};

const get = async (id: string) => {
  try {
    return await http.get<IOrganization>(`${ORGANIZATIONS_URL}/${id}`);
  } catch (error) {
    return handleHttpError(error);
  }
};

const post = async (entity: IOrganization) => {
  try {
    return await http.post<IOrganization>(ORGANIZATIONS_URL, entity);
  } catch (error) {
    return handleHttpError(error);
  }
};

const put = async (entity: IOrganization, id: string) => {
  try {
    return await http.put<IOrganization>(`${ORGANIZATIONS_URL}/${id}`, entity);
  } catch (error) {
    return handleHttpError(error);
  }
};

const remove = async (id: string) => {
  try {
    return await http.delete<IOrganization>(`${ORGANIZATIONS_URL}/${id}`);
  } catch (error) {
    return handleHttpError(error);
  }
};

const OrganizationService = {
  getAll,
  get,
  post,
  put,
  remove,
};

export default OrganizationService;
