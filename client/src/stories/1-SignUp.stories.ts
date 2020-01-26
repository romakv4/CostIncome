import { RegistrationFormComponent } from '../app/registration-form/registration-form.component';
import { moduleMetadata } from '@storybook/angular';
import { ReactiveFormsModule } from '@angular/forms';

export default {
  title: 'Sign up',
  decorators: [
      moduleMetadata({
          imports: [ ReactiveFormsModule ],
          declarations: [ RegistrationFormComponent ],
      })
  ]
};

export const ToStorybook = () => ({
  component: RegistrationFormComponent,
  props: {},
});

ToStorybook.story = {
  name: 'Sign up page',
};
