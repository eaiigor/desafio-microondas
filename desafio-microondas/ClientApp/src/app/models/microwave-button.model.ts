import {MicrowaveFunctionEnum} from "../enums/microwave-function.enum";

export interface MicrowaveButtonModel {
  display: string;
  value: string;
  type: MicrowaveFunctionEnum;
}
