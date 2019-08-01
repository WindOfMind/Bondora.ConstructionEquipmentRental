import { FetchError } from './fetch-error';

export interface Fetch<T> {
  id?: string;
  entity: T;
  error: FetchError;
  loading: boolean;
}
