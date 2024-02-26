<script setup lang="ts">
import router from '../../../router'
localStorage.removeItem("token")
router.push('/login').then(() =>{
    window.location.reload()
});
</script>