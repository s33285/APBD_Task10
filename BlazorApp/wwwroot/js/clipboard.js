export function copyToClipboard(text) {
    return navigator.clipboard.writeText(text);
}

export function confirmRemove(name) {
    return confirm(`Remove ${name} from observed students?`);
}