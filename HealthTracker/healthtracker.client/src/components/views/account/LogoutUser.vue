<script setup lang="ts">
import { updateUser } from '../../../data/service/userData'
localStorage.removeItem("user")
updateUser();
</script>