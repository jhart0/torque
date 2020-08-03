while read a; do
  echo "Checking $a Occurs exactly once in diff output"
  count=$(grep -o "$a" log | wc -l)
  if [ "$count" -ne "1" ]; then
   echo "Error, $a occured $count times";
   exit 1;
fi
done <assertions