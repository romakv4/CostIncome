import { HomeComponent } from '../app/home/home.component';

export default {
  title: 'Home',
  component: HomeComponent,
};

export const ToStorybook = () => ({
  component: HomeComponent,
  props: {},
});

ToStorybook.story = {
  name: 'Home page',
};
