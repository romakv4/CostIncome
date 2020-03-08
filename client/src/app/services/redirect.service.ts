import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RedirectService {

  delayCounter = (context: any) => {
    let interval = setInterval(() => {
        context.secondsBeforeRedirect--;
        if(context.secondsBeforeRedirect === 0) {
          clearInterval(interval);
        }
    }, 1000);
  }

  delayedRedirect = (delay: number, destination: string, router: Router) => {
    return setTimeout(() => {
        router.navigate([destination]);
    }, delay);
  }
}
