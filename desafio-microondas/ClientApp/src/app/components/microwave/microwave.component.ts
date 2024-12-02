import {Component, OnDestroy, OnInit} from '@angular/core';
import {MicrowaveHandlerService} from "../../services/microwave-handler/microwave-handler.service";
import {MicrowaveFunctionEnum} from "../../enums/microwave-function.enum";
import {MicrowaveButtonModel} from "../../models/microwave-button.model";
import {Subject, takeUntil, tap} from "rxjs";
import {MicrowaveStateEnum} from "../../enums/microwave-state.enum";

@Component({
  selector: 'app-microwave',
  templateUrl: './microwave.component.html',
  styleUrls: ['./microwave.component.scss']
})
export class MicrowaveComponent implements OnInit, OnDestroy {
  buttons: MicrowaveButtonModel[] = [];
  screen = '';
  renderedValue = '';
  power: number = 0;
  state: MicrowaveStateEnum = MicrowaveStateEnum.Idle;
  stateEnum = MicrowaveStateEnum;
  isHeatingProgram = false;

  destroy$: Subject<void> = new Subject<void>();

  constructor(private microwaveService: MicrowaveHandlerService) {
  }

  ngOnInit(): void {
    this.createButtons();
    this.renderedValue = this.renderScreen();
    this.microwaveService.startConnection().pipe(
      tap(() => this.microwaveService.joinMicrowave('1')),
      takeUntil(this.destroy$)
    ).subscribe();

    this.microwaveService.heatingProgress$
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (microwave) => {
          console.log(microwave);
          this.renderedValue = microwave.remainingTime;
          this.state = microwave.state;
          this.power = microwave.power;
        }
      })

    this.microwaveService.microwaveState$
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (state) => {
          this.state = state;

          if (this.state === MicrowaveStateEnum.Idle) {
            this.isHeatingProgram = false;
            this.screen = '';
            this.renderedValue = this.renderScreen();
          }
        }
      });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
    this.microwaveService.leaveMicrowave('1');
  }

  createButtons() {
    const buttons = new Array(9).fill(0).map((_, i) => {
      const value = i + 1;
      return {
        display: value.toString(),
        value: value.toString(),
        type: MicrowaveFunctionEnum.number
      }
    });

    buttons.push({display: 'Ligar/\n+30sec', value: 'start', type: MicrowaveFunctionEnum.start});
    buttons.push({display: '0', value: '0', type: MicrowaveFunctionEnum.number});
    buttons.push({display: 'Pausar/\nCancelar', value: 'stop', type: MicrowaveFunctionEnum.stop});

    this.buttons = buttons;
  }

  handleButtonClick(button: MicrowaveButtonModel) {
    switch (button.type) {
      case MicrowaveFunctionEnum.start:
        this.startMicrowave();
        break;
      case MicrowaveFunctionEnum.stop:
        this.stopMicrowave();
        break;
      case MicrowaveFunctionEnum.number:
        if (this.state !== MicrowaveStateEnum.Idle) return;
        this.handleNumberInput(button.value);
        break;
    }
  }

  private startMicrowave() {
    if (this.isHeatingProgram) {
      return;
    }

    if (!this.screen) {
      this.screen = '30';
      this.renderedValue = this.renderScreen();
    }

    if (this.power === 0) {
      this.power = 10;
    }

    const timeInSeconds = this.calculateTimeInSeconds();
    this.microwaveService.startMicrowave(timeInSeconds, this.power).subscribe();
  }

  private stopMicrowave() {
    switch (this.state) {
      case MicrowaveStateEnum.Heating:
        this.microwaveService.pauseMicrowave().subscribe();
        break;

      case MicrowaveStateEnum.Paused:
        this.clearScreen();
        this.microwaveService.stopMicrowave().subscribe();
        break;

      case MicrowaveStateEnum.Idle:
        this.clearScreen();
        break;
    }
  }

  private clearScreen() {
    this.screen = '';
    this.renderedValue = this.renderScreen();
  }

  private handleNumberInput(value: string) {
    if (this.screen.length >= 3) return;

    this.screen = this.screen + value;
    this.renderedValue = this.renderScreen();
  }

  renderScreen() {
    if (this.screen.length == 1) {
      return `0:0${this.screen}`;
    }

    if (this.screen.length == 2) {
      return `0:${this.screen}`;
    }

    if (this.screen.length == 3) {
      return `${this.screen[0]}:${this.screen.slice(1)}`;
    }

    if (this.screen.length == 4) {
      return `${this.screen.slice(0, 2)}:${this.screen.slice(2)}`;
    }

    return '0:00';
  }

  calculateTimeInSeconds() {
    const hasMinutes = this.screen.length > 2;
    if (!hasMinutes) return parseInt(this.screen);

    const minutes = parseInt(this.screen.slice(0, 1));
    const seconds = parseInt(this.screen.slice(1, 3));


    return minutes * 60 + seconds;
  }

  startHeatingProgram(heatingProgram: any) {
    this.isHeatingProgram = true;
    this.microwaveService.startMicrowave(10, 10, heatingProgram.id).subscribe();
  }

  handlePowerChange(power: number) {
    this.power = power;
  }
}



