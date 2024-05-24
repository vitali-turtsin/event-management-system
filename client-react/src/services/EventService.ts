import http from "../http-common";
import { IEventSearch } from "../models/searches/IEventSearch";
import { IEvent } from "../models/IEvent";
import { handleHttpError } from "../shared/helpers/http.helper";
import { toQueryString } from "../shared/helpers/http-request-util";

const EVENTS_URL = "/Events";

const getAll = async (search?: IEventSearch) => {
  try {
    return await http.get<IEvent[]>(`${EVENTS_URL}?${toQueryString(search)}`);
  } catch (error) {
    return handleHttpError(error);
  }
};

const get = async (id: string) => {
  try {
    return await http.get<IEvent>(`${EVENTS_URL}/${id}`);
  } catch (error) {
    return handleHttpError(error);
  }
};

const post = async (entity: IEvent) => {
  try {
    return await http.post<IEvent>(EVENTS_URL, entity);
  } catch (error) {
    return handleHttpError(error);
  }
};

const put = async (entity: IEvent, id: string) => {
  try {
    return await http.put<IEvent>(`${EVENTS_URL}/${id}`, entity);
  } catch (error) {
    return handleHttpError(error);
  }
};

const remove = async (id: string) => {
  try {
    return await http.delete<IEvent>(`${EVENTS_URL}/${id}`);
  } catch (error) {
    return handleHttpError(error);
  }
};

const EventService = {
  getAll,
  get,
  post,
  put,
  remove,
};

export default EventService;
