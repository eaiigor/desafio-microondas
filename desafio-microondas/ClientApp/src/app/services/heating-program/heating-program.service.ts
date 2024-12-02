import {Inject, Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {catchError, Observable, Subject, tap} from "rxjs";
import {MicrowaveStateEnum} from "../../enums/microwave-state.enum";
import {HttpClient} from "@angular/common/http";
import {MicrowaveModel} from "../../models/microwave.model";
import {fromPromise} from "rxjs/internal/observable/innerFrom";


@Injectable()
export class HeatingProgramService {
  constructor(@Inject('BASE_URL') private baseUrl: string,
              private httpClient: HttpClient
  ) {
  }

  public getHeatingPrograms(): Observable<MicrowaveModel[]> {
    return this.httpClient.get<MicrowaveModel[]>(`${this.baseUrl}api/HeatingProgram`);
  }

  public createHeatingProgram(time: number, power: number, name: string): Observable<MicrowaveModel> {
    return this.httpClient.post<MicrowaveModel>(`${this.baseUrl}api/HeatingProgram`, {
      time,
      power,
      name,
      microwaveId: '1'
    });
  }

  public deleteHeatingProgram(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}api/HeatingProgram/${id}`);
  }
}
