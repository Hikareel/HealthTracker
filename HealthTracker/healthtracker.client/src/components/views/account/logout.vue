<script setup lang="ts">
import router from '../../../router'
import { updateUser } from '../../../data/service/userData'
localStorage.removeItem("user")
updateUser();
</script>