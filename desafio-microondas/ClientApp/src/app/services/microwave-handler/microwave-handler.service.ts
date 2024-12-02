import {Inject, Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {catchError, Observable, Subject, tap} from "rxjs";
import {MicrowaveStateEnum} from "../../enums/microwave-state.enum";
import {HttpClient} from "@angular/common/http";
import {MicrowaveModel} from "../../models/microwave.model";
import {fromPromise} from "rxjs/internal/observable/innerFrom";


@Injectable()
export class MicrowaveHandlerService {
  private hubConnection!: signalR.HubConnection;
  private _heatingProgressSubject = new Subject<MicrowaveModel>();
  private _microwaveStateSubject = new Subject<MicrowaveStateEnum>();

  public get heatingProgress$(): Observable<MicrowaveModel> {
    return this._heatingProgressSubject.asObservable();
  }

  public get microwaveState$(): Observable<MicrowaveStateEnum> {
    return this._microwaveStateSubject.asObservable();
  }


  constructor(@Inject('BASE_URL') private baseUrl: string,
              private httpClient: HttpClient
  ) {
  }

  public startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.baseUrl + 'microwaveHub')
      .configureLogging(signalR.LogLevel.Information)
      .build();

    return fromPromise(
      this.hubConnection.start()
    ).pipe(tap(() => {
        console.log('Connection started');
        this.addMicrowaveListeners();
      }),
      catchError(() => {
        console.log('Error while starting connection');
        return new Observable
      }))
  }

  private addMicrowaveListeners() {
    this.hubConnection.on('MicrowaveState', (state: MicrowaveStateEnum) => {
      this._microwaveStateSubject.next(state);
    });

    this.hubConnection.on('HeatingProgress', (microwave: MicrowaveModel) => {
      this._heatingProgressSubject.next(microwave);
    });
  }

  public joinMicrowave(microwaveId: string) {
    this.hubConnection
      .invoke('JoinMicrowaveGroup', microwaveId);
  }

  public leaveMicrowave(microwaveId: string) {
    this.hubConnection.invoke('LeaveMicrowaveGroup', microwaveId);
  }

  public startMicrowave(time: number, power: number, heatingProgramId: number | null = null) {
    return this.httpClient.post<MicrowaveModel>(`${this.baseUrl}api/microwave/start`, {
      time,
      power,
      heatingProgramId
    });
  }

  public pauseMicrowave() {
    return this.httpClient.post<MicrowaveModel>(`${this.baseUrl}api/microwave/pause`, {});
  }

  public stopMicrowave() {
    return this.httpClient.post<MicrowaveModel>(`${this.baseUrl}api/microwave/stop`, {});
  }
}
