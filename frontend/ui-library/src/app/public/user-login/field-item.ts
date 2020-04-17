import { FormControl } from "@angular/forms";

export interface IFieldItem {
  property: string;
  editor: string;
  formControl: FormControl;
  type?: object | string | Date | number | null;
  format?: string;
  maxLength?: number;
  minLength?: number;
  label?: string;
  required?: boolean;
  compare?: string;
  defaultValue?: any;
}
