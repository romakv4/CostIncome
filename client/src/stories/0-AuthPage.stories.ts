import { AuthPageComponent } from '../app/auth-page/auth-page.component';

export default {
  title: 'Home',
  component: AuthPageComponent,
};

export const ToStorybook = () => ({
  component: AuthPageComponent,
  props: {},
});

ToStorybook.story = {
  name: 'Home page',
};
