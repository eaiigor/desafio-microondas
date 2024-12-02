import {MicrowaveStateEnum} from "../enums/microwave-state.enum";

export interface MicrowaveModel {
  remainingTime: string;
  power: number;
  state: MicrowaveStateEnum
  isHeatingProgram: boolean;
}
