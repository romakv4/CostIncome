import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorsService {

  resetServerErrors(context: any) {
    if (context.serverErrors !== null) return context.serverErrors = null;
  }
}
