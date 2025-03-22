import { inject, Injectable, SecurityContext } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { DomSanitizer } from "@angular/platform-browser";

@Injectable({
  providedIn: 'root'
})
export class SanitizerUtil{
  private sanitizer = inject(DomSanitizer)

  sanitizeValue(value: any): string {
    if (typeof value === 'string') {
      const sanitized = this.sanitizer.sanitize(SecurityContext.HTML, value);
      return sanitized ? sanitized : '';
    }
    return value;
  }

  sanitizeUrl(value: string): string {
    const sanitized = this.sanitizer.sanitize(SecurityContext.URL, value);
    return sanitized ? sanitized.toString() : '';
  }

  sanitizeForm(form: FormGroup): any {
    const sanitizedValues: any = {};

    Object.keys(form.controls).forEach((key) => {
      const control = form.controls[key];
      sanitizedValues[key] = this.sanitizeValue(control.value);
    });

    return sanitizedValues;
  }
}
