import { AuthorizationFormComponent } from '../app/authorization-form/authorization-form.component';
import { moduleMetadata } from '@storybook/angular';
import { ReactiveFormsModule } from '@angular/forms';

export default {
  title: 'Sign in',
  decorators: [
      moduleMetadata({
          imports: [ ReactiveFormsModule ],
          declarations: [ AuthorizationFormComponent ],
      })
  ]
};

export const ToStorybook = () => ({
  component: AuthorizationFormComponent,
  props: {},
});

ToStorybook.story = {
  name: 'Sign in page',
};
